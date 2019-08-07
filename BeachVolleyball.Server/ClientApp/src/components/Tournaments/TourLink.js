import React from "react";
import { Link } from "react-router-dom";
import { NavItem, NavLink } from "reactstrap";

export  const TournamentLink = ({ name, id, categories }) => (
  <NavItem>
    <NavLink tag={Link} to={createTournamentPath(id, categories[0].id)}>
      {name}
    </NavLink>
  </NavItem>
);

export function createTournamentPath(id, categoryId) {
  return "/tournaments/" + id + "/" + categoryId;
}

