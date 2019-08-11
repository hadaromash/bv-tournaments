import React from "react";
import {
  Container,
  Navbar,
  NavbarBrand,
} from "reactstrap";
import { Link } from "react-router-dom";
import "./NavMenu.css";

const NavMenu = () => {
  return (
    <header>
      <Navbar
        className="border-bottom box-shadow mb-3"
        dark expand="sm"
        style={{backgroundColor: '#09058D'}}
      >
        <Container>
          <NavbarBrand tag={Link} to="/">
            הסבב הישראלי בכדורעף חופים
          </NavbarBrand>
      { /*   <NavbarToggler onClick={toggleNavbar} className="mr-2" />
          <Collapse
            className="d-sm-inline-flex flex-sm-row-reverse"
            isOpen={!state.collapsed}
            navbar
          >
            <ul className="navbar-nav flex-grow">{tournamentsMenu}</ul>
          </Collapse> */}
        </Container>
      </Navbar>
    </header>
  );
};

export default NavMenu;
