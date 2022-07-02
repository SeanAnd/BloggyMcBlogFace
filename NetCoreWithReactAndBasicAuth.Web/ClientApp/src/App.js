import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import Blog from './components/blog/Blog';
import LoginPage from './components/login/Login';
import SignupPage from './components/signup/Signup';
import './custom.css'
import { PrivateRoute } from './components/shared/PrivateRoute';
import { AuthProvider } from "./components/shared/context/Context";

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <AuthProvider>
        <Layout>
          <Route exact path="/" component={Home} />
          <PrivateRoute path="/blog" component={Blog} />
          <Route path="/login" component={LoginPage} />
          <Route path="/signup" component={SignupPage} />
        </Layout>
      </AuthProvider>
    );
  }
}
