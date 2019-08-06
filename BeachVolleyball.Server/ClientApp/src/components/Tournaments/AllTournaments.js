import React, { useState, useEffect } from "react";
import TournamentApi from "../../api/TournamentsApi";
import Tournament from "./Tournament";

const Tournaments = () => {
  var initialState = {
    tournaments: [],
    loading: true,
    error: false
  };
  const [state, setState] = useState(initialState);

  useEffect(() => {
    const setTours = async () => {
      var api = new TournamentApi();

      try {
        var tours = await api.getTournaments();
        console.log("Received all tournaments");
        setState({
          tournaments: tours,
          loading: false,
          error: false
        });
      } catch (error) {
        console.error("Failed to get tournaments info");
        setState({
          tournaments: null,
          loading: false,
          error: true
        });
      }
    };

    setTours();
  }, []);

  let tournamentView = <div />;
  if (state.tournaments.length > 0) {
    tournamentView = <Tournament {...state.tournaments[0]} />;
  }

  return <div>{tournamentView}</div>;
};

export default Tournaments;
