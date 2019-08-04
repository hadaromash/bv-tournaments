import React, { useEffect, useState } from "react";
import TournamentApi from "../../api/TournamentsApi";
import SelectionForm from "./SelectionForm";
import Pools from "./Pools";
import styled from "styled-components";
import { Spinner } from 'reactstrap';

export const TournamentSelection = () => {
  var initialState = {
    tournaments: null,
    categories: null,
    pools: null,
    loadingPools: false,
    loadingPoolsError: false
  };
  const [state, setState] = useState(initialState);

  const showPools = async values => {
    setState({
      ...state,
      pools: null,
      loadingPools: true,
      loadingPoolsError: false
    });

    var api = new TournamentApi();
    try {
      const pools = await api.getPools(values.tournament, values.category);
      setState({
        ...state,
        pools: pools,
        loadingPools: false,
        loadingPoolsError: false
      });
    } catch (error) {
      console.log("Failed to get pools");
      setState({
        ...state,
        loadingPools: false,
        loadingPoolsError: true
      });
    }
  };

  useEffect(() => {
    const setTours = async () => {
      var api = new TournamentApi();
      var tours = await api.getTournaments();
      var categories = await api.getCategories(tours[0].tournamentId);
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

  let loadingPools = null;
  if (state.loadingPools) {
    loadingPools = <Loading/>;
  }

  let loadingPoolsError = null;
  if (state.loadingPoolsError) {
    loadingPoolsError = <p>אוף! לא הצלחתי למשוך את המידע מהשרת :(</p>;
  }

  return (
    <TournamentsContainer>
      <SelectionForm {...state} handleSubmit={showPools} />
      {pools}
      {loadingPools}
      {loadingPoolsError}
    </TournamentsContainer>
  );
};

const TournamentsContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: start;

  & > * {
    margin-bottom: 2rem;
  }
`;

const Loading = () => (
  <LoadingContainer>
    <Spinner type="grow" color="primary" />
    <Spinner type="grow" color="secondary" />
    <Spinner type="grow" color="success" />
    <Spinner type="grow" color="danger" />
    <Spinner type="grow" color="warning" />
    <Spinner type="grow" color="info" />
    <Spinner type="grow" color="dark" />
  </LoadingContainer>
);

const LoadingContainer = styled.div`
  align-self: center;
  justify-self: center;
`
