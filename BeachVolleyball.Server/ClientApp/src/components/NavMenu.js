import React, { useState, useContext } from "react";
import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  DropdownItem,
  NavItem,
  NavLink,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu
} from "reactstrap";
import { Link } from "react-router-dom";
import "./NavMenu.css";
import { TournamentsContext } from "../Tournaments.context";

const NavMenu = () => {
  const [state, setState] = useState({ collapsed: true });
  const { tournamentsState } = useContext(TournamentsContext);

  const toggleNavbar = () => {
    setState({
      collapsed: !state.collapsed
    });
  };

  let tournamentsMenu = null;
  if (tournamentsState.tournaments.length > 0) {
    const tournamentLinks = tournamentsState.tournaments.map(tour => (
      <DropdownItem key={tour.id}>
        <NavItem>
          <NavLink
            tag={Link}
            className="text-dark"
            to={"/tournaments/" + tour.id}
          >
            {tour.name}
          </NavLink>
        </NavItem>
      </DropdownItem>
    ));

    tournamentsMenu = (
      <UncontrolledDropdown nav inNavbar>
        <DropdownToggle nav caret>
        &nbsp;טורנירים&nbsp;
        </DropdownToggle>
        <DropdownMenu>{tournamentLinks}</DropdownMenu>
      </UncontrolledDropdown>
    );
  }

  return (
    <header>
      <Navbar
        className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
        light
      >
        <Container>
          <NavbarBrand tag={Link} to="/">
            הסבב הישראלי בכדורעף חופים
          </NavbarBrand>
          <NavbarToggler onClick={toggleNavbar} className="mr-2" />
          <Collapse
            className="d-sm-inline-flex flex-sm-row-reverse"
            isOpen={!state.collapsed}
            navbar
          >
            <ul className="navbar-nav flex-grow">{tournamentsMenu}</ul>
          </Collapse>
        </Container>
      </Navbar>
    </header>
  );
};

export default NavMenu;
