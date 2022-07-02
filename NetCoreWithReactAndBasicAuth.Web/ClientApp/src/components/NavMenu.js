import React from "react";
import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
} from "reactstrap";
import { Link } from "react-router-dom";
import "./NavMenu.css";
import { useAuthState, useAuthDispatch } from '../components/shared/context/Context'
import { logout } from '../components/shared/context/Actions'

const NavMenu = () => {
  const dispatch = useAuthDispatch()
  const { user } = useAuthState()

  const handleLogout = () => {
    logout(dispatch)
}

  return (
    <header>
      <Navbar
        className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
        light
      >
        <Container>
          <NavbarBrand tag={Link} to="/">
            Bloggy McBlogFace
          </NavbarBrand>
          <NavbarToggler className="mr-2" />
          <Collapse
            className="d-sm-inline-flex flex-sm-row-reverse"
            isOpen={false}
            navbar
          >
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/">
                  Home
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/blog">
                  Blog
                </NavLink>
              </NavItem>
              <NavItem>
                {user ? (
                  <NavLink tag={Link} onClick={handleLogout} className="text-dark" to="/login">
                    Logout
                  </NavLink>
                ) : (
                  <NavLink tag={Link} className="text-dark" to="/login">
                    Login
                  </NavLink>
                )}
              </NavItem>
            </ul>
          </Collapse>
        </Container>
      </Navbar>
    </header>
  );
};

export default NavMenu