import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Oh, hi Mark!</h1>
        <p>This is a demo blog site</p>
        <p>This app includes basic authorization and authentication as well as private routes.</p>
        <ul>
          <li><Link to={"/signup"}><strong>Signup</strong></Link>. Click the login button at the top right and then the signup button below username and password.</li>
          <li><Link to={"/login"}><strong>Login</strong></Link>. Once you have logged in you can view posts and even delete/edit your own post.</li>
          <li><Link to={"/blog"}><strong>Blog</strong></Link>. Once logged in, say what is on your mind and share some related imagery.</li>
        </ul>
      </div>
    );
  }
}
