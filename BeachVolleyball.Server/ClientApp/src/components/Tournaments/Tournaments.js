import React, { useEffect, useState } from "react";
import TournamentApi from "../../api/TournamentsApi";
import SelectionForm from "./SelectionForm";
import Pools from "./Pools";

export const TournamentSelection = () => {
  var initialState = {
    tournaments: null,
    categories: null,
    pools: null
  };
  const [state, setState] = useState(initialState);

  const showPools = async values => {
    var api = new TournamentApi();
    const pools = await api.getPools(values.tournament, values.category);
    setState({
      ...state,
      pools: pools
    });
  };

  useEffect(() => {
    const setTours = async () => {
      var api = new TournamentApi();
      var tours = await api.getTournaments();
      var categories = await api.getCategories();
      setState({
        tournaments: tours,
        categories: categories
      });
    };

    setTours();
  }, []);

  if (!state.tournaments || !state.categories) {
    return <div />;
  }

  let pools = null;
  if (state.pools) {
    pools = <Pools pools={state.pools} />;
  }

  return (
    <div>
      <SelectionForm {...state} handleSubmit={showPools} />
      {pools}
    </div>
  );
};
