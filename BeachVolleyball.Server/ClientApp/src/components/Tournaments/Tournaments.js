import React, { useContext } from 'react';
import { TournamentsContext } from '../../Tournaments.context';
import Tournament from './Tournament';
import { Redirect } from 'react-router';

const Tournaments = ({match}) => {
    const { tournamentsState } = useContext(TournamentsContext);
    console.log("Showing tournament of id: " + match.params.id);

    function findTournament(tour) {
        return tour.id === match.params.id;
    }

    const currentTour = tournamentsState.tournaments.find(findTournament);
    if (currentTour) {
        return (<Tournament {...currentTour} />)
    }
    
    return (<Redirect to="/" />)
}

export default Tournaments;