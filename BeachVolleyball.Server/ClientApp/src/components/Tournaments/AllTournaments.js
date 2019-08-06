import React, { useState} from "react";
import Tournament from "./Tournament";

const Tournaments = () => {
  var initialState = {
    tournaments: [],
    loading: true,
    error: false
  };
  const [state, setState] = useState(initialState);

  

  let tournamentView = <div />;
  if (state.tournaments.length > 0) {
    tournamentView = <Tournament {...state.tournaments[0]} />;
  }

  return <div>{tournamentView}</div>;
};

export default Tournaments;
