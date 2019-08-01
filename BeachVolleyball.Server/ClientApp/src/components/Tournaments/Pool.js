import React from 'react';
import styled from 'styled-components';
import Team from './Team';
import Match from './Match';

const Pool = ({number, teams, qualificationMatches}) => {
    const teamViews = teams.map(team => <Team {...team}/>)
    let qualMatchesTitle = null;
    let qualificationMatchesViews = null;
    if (qualificationMatches && qualificationMatches.length > 0) {
        qualMatchesTitle = <p>Qualification matches:</p>;
        qualificationMatchesViews = qualificationMatches.map(match => <Match {...match}/>)
    }
    
    return (
        <PoolContainer>
            <h6>בית {number}</h6>
            {teamViews}
            {qualMatchesTitle}
            {qualificationMatchesViews}
        </PoolContainer>
    )
}

const PoolContainer = styled.div`
    display: flex;
    flex-direction: column;
    justify-content: center;
`

export default Pool;