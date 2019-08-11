import React, { useState, useContext } from "react";
import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  DropdownItem,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu
} from "reactstrap";
import { Link } from "react-router-dom";
import "./NavMenu.css";
import { TournamentsContext } from "../Tournaments.context";
import { TournamentLink } from "./Tournaments/TourLink";

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
        <TournamentLink {...tour} />
      </DropdownItem>
    ));

    tournamentsMenu = (
      <UncontrolledDropdown nav inNavbar>
        <DropdownToggle nav>
        &nbsp;טורנירים&nbsp;
        </DropdownToggle>
        <DropdownMenu>{tournamentLinks}</DropdownMenu>
      </UncontrolledDropdown>
    );
  }

  return (
    <header>
      <Navbar
        className="border-bottom box-shadow mb-3"
        light expand="sm"
        style={{backgroundColor: '#e2edf4'}}
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
