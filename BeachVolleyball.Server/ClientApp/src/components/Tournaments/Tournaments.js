import React, { useContext } from 'react';
import { TournamentsContext } from '../../Tournaments.context';
import Tournament from './Tournament';
import { Redirect } from 'react-router';
import Spinner from "react-bootstrap/Spinner";
import styled from 'styled-components';

const Tournaments = ({match}) => {
    const { tournamentsState } = useContext(TournamentsContext);
    console.log("Showing tournament of id: " + match.params.id);

    function findTournament(tour) {
        return tour.id === match.params.id;
    }

    if (tournamentsState.loading) {
        return (
            <LoadingContainer>
                <p>טוען מידע...</p>
                <Spinner animation="border" variant="info" />
            </LoadingContainer>
        );
    }

    const currentTour = tournamentsState.tournaments.find(findTournament);
    if (currentTour) {
        return (<Tournament {...currentTour} match={match}/>)
    }
    
    return (<Redirect to="/" />)
}

export default Tournaments;

const LoadingContainer = styled.div`
    margin-top: 2rem;

    display: flex;
    flex-direction: row;
    & > * {
        margin-left: 2rem;
    }
`