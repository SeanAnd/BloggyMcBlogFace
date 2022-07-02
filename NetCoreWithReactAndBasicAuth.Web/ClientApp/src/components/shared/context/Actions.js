const apiUrl = 'https://localhost:7121';

export async function loginUser(dispatch, loginPayload) {
  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(loginPayload),
  };

  try {
    dispatch({ type: 'REQUEST_LOGIN' });
    let response = await fetch(`${apiUrl}/user/authenticate`, requestOptions);
    let data = await response.json();

    if (data) {
      dispatch({ type: 'LOGIN_SUCCESS', payload: data });
      data.authdata = window.btoa(data.username + ':' + data.password);
      localStorage.setItem('user', JSON.stringify(data));
      return data
    }

    dispatch({ type: 'LOGIN_ERROR', error: "login error" });
    return;
  } catch (error) {
    dispatch({ type: 'LOGIN_ERROR', error: error });
  }
}

export async function signupUser(dispatch, signupPayload) {
  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(signupPayload),
  };

  try {
    dispatch({ type: 'REQUEST_SIGNUP' });
    let response = await fetch(`${apiUrl}/user/signup`, requestOptions);
    let data = await response.json();

    if (data) {
      dispatch({ type: 'SIGNUP_SUCCESS', payload: data });
      data.authdata = window.btoa(data.username + ':' + data.password);
      localStorage.setItem('user', JSON.stringify(data));
      return data
    }

    dispatch({ type: 'SIGNUP_ERROR', error: "signup error" });
    return;
  } catch (error) {
    dispatch({ type: 'SIGNUP_ERROR', error: error });
  }
}

export async function logout(dispatch) {
  dispatch({ type: 'LOGOUT' });
  localStorage.removeItem('user');
  window.location.reload(true);
}