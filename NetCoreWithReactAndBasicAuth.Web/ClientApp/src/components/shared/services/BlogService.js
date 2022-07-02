import { authHeader, authHeaderForm } from '../../../helpers/AuthHeader';

const apiUrl = 'https://localhost:7121';

export const blogService = {
    get,
    save,
    remove
};

function get() {
    const requestOptions = {
        method: 'GET',
        headers: authHeaderForm(),
    };

    return fetch(`${apiUrl}/post`, requestOptions)
        .then(handleResponse);
}

function save(id, data) {
    // for other requests requiring auth
    const requestOptions = {
        method: id ? 'PUT' : 'POST',
        headers: authHeaderForm(),
        body: data
    };
    return fetch(`${apiUrl}/post/` + (id ? `${id}` : 'create'), requestOptions)
        .then(handleResponse);
}

function remove(id) {
    // for other requests requiring auth
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader()
    };
    
    return fetch(`${apiUrl}/post/${id}`, requestOptions)
        .then(handleResponse);
}

function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}