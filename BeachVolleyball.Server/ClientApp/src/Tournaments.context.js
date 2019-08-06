import React, { useState, useEffect } from "react";
import TournamentApi from "./api/TournamentsApi";

const TournamentsContext = React.createContext();
const { Provider } = TournamentsContext;
// const  Provider = MessageContext.Provider;

const TournamentsProvider = ({ children }) => {
  const [tournamentsState, setTournamentsState] = useState({
      tournaments: [],
      loading: false,
      error: false
  });

  useEffect(() => {
    const setTours = async () => {
      console.log("Getting tournaments...");
      var api = new TournamentApi();

      try {
        var tours = await api.getTournaments();
        console.log("Succesfully received " + tournamentsState.tournaments.length + " tournaments");
        setTournamentsState({
          tournaments: tours,
          loading: false,
          error: false
        });
      } catch (error) {
        console.error("Failed to get tournaments");
        setTournamentsState({
          tournaments: [],
          loading: false,
          error: true
        });
      }
    };

    setTours();
  }, []);

  // state = valuyes to display
  const state = {
    tournamentsState
  };
  // actions = callbacks to invoke
  const actions = {};

  return <Provider value={{ ...state, ...actions }}> {children} </Provider>;
};

export { TournamentsProvider, TournamentsContext };
