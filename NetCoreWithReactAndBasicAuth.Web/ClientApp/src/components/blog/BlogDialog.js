import {
  TextField,
  Button,
  Box,
  Paper,
  CircularProgress,
  DialogTitle,
  Dialog,
  DialogContent,
  DialogActions,
  TextareaAutosize,
  Grid,
  Tooltip
} from "@material-ui/core";
import React, { useState, useEffect } from "react";
import "./BlogStyles.css";

const BlogDialog = (props) => {
  const [errorText, setErrorText] = useState("");

  useEffect(() => {
    var errorText = "";
    if(!props?.data?.photos?.length > 0)
    {
      errorText = "You must select at least one photo. "
    }
    if(!props?.data?.title.length > 0)
    {
      errorText = errorText + "You must add a title. "
    }
    if(!props?.data?.content.length > 0)
    {
      errorText = errorText + "You must add content. "
    }
    setErrorText(errorText)
  }, [props?.data]);

  const handleChange = (event) => {
    props.setData({ ...props.data, [event.target.name]: event.target.value });
  };

  const onImagesPicked = (event) => {
    if (!event.target.files || event.target.files.length === 0) {
      return;
    }
    var files = Array.from(event.target.files);
    var photoArray = [];
    files.forEach(f => {
      var photoObject = {
        id: 0,
        path: undefined,
        postId: 0,
        file: f,
        name: f.name,
      };
      photoArray.push(photoObject);
    })
    props.setData({ ...props.data, photos: photoArray });
  };

  return (
    <Box className="blogDialog">
      {props.loading ? (
        <Paper className="loading">
          <CircularProgress />
        </Paper>
      ) : (
        <Dialog fullWidth maxWidth={"sm"} open={props.open}>
          <DialogTitle>
            <TextField
              fullWidth
              type="text"
              onChange={handleChange}
              value={props?.data?.title ?? ""}
              name="title"
              label="Title"
              required={true}
              error={props?.data?.title === ""}
            />
          </DialogTitle>
          <Button component="label" color="primary">
            Upload Photos
            <input
              type="file"
              accept="image/*"
              multiple
              hidden
              onChange={onImagesPicked}
            />
          </Button>
          <Grid
            container
            direction="row"
            justifyContent="center"
            alignItems="center"
            wrap="nowrap"
            className="iList"
          >
            {props.data?.photos?.length > 0
              ? props.data?.photos.map((image) => {
                  var source = image?.path ?? URL.createObjectURL(image.file);
                  return (
                    <Grid item key={image.name} className="imageItem">
                      <img src={source} alt={image.name} className="image" />
                    </Grid>
                  );
                })
              : []}
          </Grid>
          <DialogContent>
            <TextareaAutosize
              name="content"
              style={{ width: "100%" }}
              minRows={5}
              onChange={handleChange}
              value={props?.data?.content ?? ""}
              placeholder="Content *"
            />
            <DialogActions>
              <Button
                onChange={handleChange}
                onClick={props.onCancel}
                variant="contained"
                color="secondary"
              >
                Cancel
              </Button>
              <Tooltip title={errorText}>
                <span>
                  <Button
                    onClick={props.onSubmit}
                    variant="contained"
                    color="primary"
                    disabled={errorText !== ""}
                  >
                    Submit
                  </Button>
                </span>
              </Tooltip>
            </DialogActions>
          </DialogContent>
        </Dialog>
      )}
    </Box>
  );
};

export default BlogDialog;
