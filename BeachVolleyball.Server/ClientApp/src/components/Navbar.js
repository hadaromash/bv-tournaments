import React, { useContext } from "react";
import Navbar from "react-bootstrap/Navbar";
import Nav from "react-bootstrap/Nav";
import NavDropdown from "react-bootstrap/NavDropdown";
import { TournamentsContext } from "../Tournaments.context";

const NavMenu = () => {
  const { tournamentsState } = useContext(TournamentsContext);
  console.log({tournamentsState});

  let tournamentsDropdown = null;
  if (tournamentsState.tournaments.length > 0) {
    const tournamentNavs = tournamentsState.tournaments.map(tournament => (
      <NavDropdown.Item href={"#" + tournament.id}>
        {tournament.name}
      </NavDropdown.Item>
    ));

    tournamentsDropdown = (
      <NavDropdown title="טורנירים" id="basic-nav-dropdown">
        {tournamentNavs}
      </NavDropdown>
    );
  }

  return (
    <Navbar bg="light" expand="lg">
      <Navbar.Brand href="#">הסבב הישראלי בכדורעף חופים</Navbar.Brand>
      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav className="mr-auto">{tournamentsDropdown}</Nav>
      </Navbar.Collapse>
    </Navbar>
  );
};

export default NavMenu;
