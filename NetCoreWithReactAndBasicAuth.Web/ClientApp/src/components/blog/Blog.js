import {
  Button,
  Box,
  Paper,
  CircularProgress,
  Accordion,
  AccordionActions,
  AccordionDetails,
  AccordionSummary,
  Fab,
  Divider,
  Grid,
  TextareaAutosize
} from "@material-ui/core";
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import React, { useEffect, useState } from "react";
import AddIcon from '@material-ui/icons/Add';
import BlogDialog from "./BlogDialog";
import { blogService } from "../shared/services/BlogService"
import { useAuthState } from '../shared/context/Context'
import "./BlogStyles.css";


const Blog = () => {
  const [data, setData] = useState([]);
  const [blog, setBlog] = useState({
    id: 0,
    title: "",
    content: "",
    userId: 0,
    photos: []
  });
  const [loading, setLoading] = useState(true);
  const [dialogOpen, setDialogOpen] = useState(false);
  const { user } = useAuthState()

  useEffect(() => {
    getBlog()
  }, []);

  async function getBlog() {
    try {
      var data = await blogService.get();
      setData(data);
    } catch {
    } finally {
      setLoading(false);
    }
  }

  function onCreateClick() {
    setDialogOpen(true);
    setBlog({
      id: 0,
      title: "",
      content: "",
      userId: 0,
      photos: [],
    });
  }

  function onEditClick(event) {
    setDialogOpen(true);
    setBlog(event);
  }

  async function onDeleteClick(id) {
    setLoading(true);
    try {
      await blogService.remove(id)
      await getBlog();
    } catch {
      setLoading(false);
    } 
  }

  async function onDialogSubmit() {
    setLoading(true);
    const formData = new FormData();
    formData.append("id", blog.id)
    formData.append("title", blog.title)
    formData.append("content", blog.content)
    formData.append("user", blog.user)
    formData.append("userId", blog.userId)
    blog.photos.forEach((p, index) => {
      Object.keys(p).forEach(key => {
        formData.append(`photos[${index}].${key}`, p[key])
      })
    })
    try {
      await blogService.save(blog.id, formData);
      setDialogOpen(false);
      setBlog({
        id: null,
        title: "",
        content: "",
        userId: 0,
        photos: [],
      });
    } catch {
    } finally {
      await getBlog();
    }
  }

  function onDialogCancel() {
    setDialogOpen(false);
    setBlog({
      id: null,
      title: "",
      content: "",
      userId: 0,
      photos: [],
    });
  }

  return (
    <Box className="blogBox">
      {loading ? (
        <Paper className="loading">
          <CircularProgress />
        </Paper>
      ) : (
        <>
          <div className="btnDiv">
            <Fab
              className="addButton"
              variant="extended"
              size="medium"
              color="primary"
              aria-label="add"
              onClick={onCreateClick}
            >
              {/* insert programming humor here */}
              Blog
              <AddIcon />
              <AddIcon />
            </Fab>
          </div>
          {data?.length > 0 ? (
            data?.map((d) => {
              return (
                <Accordion key={d.id}>
                  <AccordionSummary expandIcon={<ExpandMoreIcon />}>
                    {d.title} By: {d.user?.username ?? "Anon"}
                  </AccordionSummary>
                  <Divider />
                  <Grid
                    container
                    direction="row"
                    justifyContent="center"
                    alignItems="center"
                    wrap="nowrap"
                    className="iList"
                  >
                    {d?.photos?.length > 0
                      ? d?.photos.map((image) => {
                          var source = image.path;
                          return (
                            <Grid item key={image.name} className="imageItem">
                              <img
                                src={source}
                                alt={image.name}
                                className="image"
                              />
                            </Grid>
                          );
                        })
                      : []}
                  </Grid>
                  <Divider />
                  <AccordionDetails>
                    <TextareaAutosize
                      name="content"
                      style={{ width: "100%" }}
                      minRows={5}
                      value={d.content}
                      disabled={true}
                    />
                  </AccordionDetails>
                  <AccordionActions>
                    {d.user?.id === user.id ? (
                      <>
                        <Button
                          variant="contained"
                          color="secondary"
                          onClick={() => onDeleteClick(d.id)}
                        >
                          Delete
                        </Button>
                        <Button
                          variant="contained"
                          color="primary"
                          onClick={() => onEditClick(d)}
                        >
                          Edit
                        </Button>
                      </>
                    ) : (
                      <></>
                    )}
                  </AccordionActions>
                </Accordion>
              );
            })
          ) : (
            <>No Blogs found!</>
          )}
          <BlogDialog
            open={dialogOpen}
            loading={loading}
            onCancel={onDialogCancel}
            onSubmit={onDialogSubmit}
            data={blog}
            setData={setBlog}
          />
        </>
      )}
    </Box>
  );
};

export default Blog;
