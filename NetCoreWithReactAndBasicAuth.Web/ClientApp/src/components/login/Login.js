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
import "./LoginStyles.css";
import { useAuthState, useAuthDispatch } from '../shared/context/Context'
import { loginUser, logout } from '../shared/context/Actions'

const LoginPage = () => {
  const [data, setData] = useState({ username: "", password: "" });
  const [loading, setLoading] = useState(true);
  const history = useHistory();
  const dispatch = useAuthDispatch()
  const { user } = useAuthState()
  const [errorText, setErrorText] = useState("");

  useEffect(() => {
    if(user) {
      logout(dispatch) //call the logout action
    }
    setLoading(false);
  // eslint-disable-next-line react-hooks/exhaustive-deps
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
    setErrorText(errorText)
  }, [data]);

  function onSignup() {
    history.push('/signup')
  }

  async function onLogin() {
    try {
      setLoading(true);
      let response = await loginUser(dispatch, data)
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
    <Box className="box">
      {loading ? (
        <Paper className="loading">
          <CircularProgress />
        </Paper>
      ) : (
        <FormGroup>
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
          <Button
            type="button"
            color="primary"
            className="signup"
            onClick={onSignup}
          >
            Signup
          </Button>
          <Tooltip title={errorText}>
            <span>
              <Button
                variant="contained"
                type="button"
                color="primary"
                className="login"
                onClick={onLogin}
                disabled={errorText !== ""}
              >
                Login
              </Button>
            </span>
          </Tooltip>
        </FormGroup>
      )}
    </Box>
  );
};

export default LoginPage;
