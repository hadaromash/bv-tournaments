import React from "react";
import { Link } from "react-router-dom";
import { NavItem, NavLink } from "reactstrap";

const TournamentLink = ({ name, id }) => (
  <NavItem>
    <NavLink tag={Link} to={"/tournaments/" + id}>
      {name}
    </NavLink>
  </NavItem>
);

export default TournamentLink;
