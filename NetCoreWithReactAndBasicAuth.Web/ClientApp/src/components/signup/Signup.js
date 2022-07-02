import {
  TextField,
  Button,
  Box,
  Paper,
  CircularProgress,
  FormGroup,
  Tooltip
} from "@material-ui/core";
import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { signupUser } from "../shared/context/Actions";
import "./SignupStyles.css";
import { useAuthDispatch } from '../shared/context/Context'

const SignupPage = () => {
  const [data, setData] = useState({
    firstname: "",
    lastname: "",
    username: "",
    password: "",
  });
  const [loading, setLoading] = useState(true);
  const history = useHistory();
  const dispatch = useAuthDispatch()
  const [errorText, setErrorText] = useState("");

  useEffect(() => {
    setLoading(false);
  }, []);

  useEffect(() => {
    var errorText = "";
    if(!data?.username?.length > 0)
    {
      errorText = "Please enter your username. "
    }
    if(!data?.password.length > 0)
    {
      errorText = errorText + "Please enter your password. "
    }
    if(!data?.firstname.length > 0)
    {
      errorText = errorText + "Please enter your first name. "
    }
    if(!data?.lastname.length > 0)
    {
      errorText = errorText + "Please enter your last name. "
    }
    setErrorText(errorText)
  }, [data]);

  async function onSignup() {
    try {
      setLoading(true);
      let response = await signupUser(dispatch, data);
      if (!response) return
      history.push("/");
    } catch {
      setLoading(false);
    }
  }

  const handleChange = (event) => {
    setData({ ...data, [event.target.name]: event.target.value });
  };

  return (
    <Box className="signupBox">
      {loading ? (
        <Paper className="loading">
          <CircularProgress />
        </Paper>
      ) : (
        <FormGroup>
          <TextField
            label="First Name"
            name="firstname"
            onChange={handleChange}
            value={data?.firstname}
            type="text"
            variant="filled"
            required
          />
          <TextField
            label="Last Name"
            name="lastname"
            onChange={handleChange}
            value={data?.lastname}
            type="text"
            variant="filled"
            required
          />
          <TextField
            label="Username"
            name="username"
            onChange={handleChange}
            value={data?.username}
            type="text"
            variant="filled"
            required
          />
          <TextField
            label="Password"
            name="password"
            onChange={handleChange}
            value={data?.password}
            type="password"
            variant="filled"
            required
          />
          <Tooltip title={errorText}>
            <span>
              <Button
                variant="contained"
                type="button"
                color="primary"
                onClick={onSignup}
                className="signup"
                disabled={errorText !== ""}
              >
                Signup
              </Button>
            </span>
          </Tooltip>
        </FormGroup>
      )}
    </Box>
  );
};

export default SignupPage;
