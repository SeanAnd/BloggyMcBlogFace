export function authHeader() {
    // return authorization header with basic auth credentials
    let user = JSON.parse(localStorage.getItem('user'));

    if (user && user.authdata) {
        return {
          Authorization: "Basic " + user.authdata,
          "Content-Type": "application/json",
        };
    } else {
        return {};
    }
}

export function authHeaderForm() {
    // return authorization header with basic auth credentials
    let user = JSON.parse(localStorage.getItem('user'));

    if (user && user.authdata) {
        return {
          Authorization: "Basic " + user.authdata,
        };
    } else {
        return {};
    }
}