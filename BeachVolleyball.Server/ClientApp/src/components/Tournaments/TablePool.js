import React from "react";
import styled from "styled-components";
import Match from "./Match";

const Pool = ({ number, teams, qualificationMatches }) => {
  const teamViews = teams.map((team, index) => (
    <React.Fragment key={team.player1.name + team.player2.name}>
      <TeamBackground row={index * 2 + 3} teamIndex={index} />
      <Rank row={index * 2 + 3}>{team.rank}</Rank>
      <Player row={index * 2 + 3}>{team.player1.name}</Player>
      <Player row={index * 2 + 4}>{team.player2.name}</Player>
    </React.Fragment>
  ));

  let qualMatchesTitle = null;
  let qualificationMatchesViews = null;
  if (qualificationMatches && qualificationMatches.length > 0) {
    qualMatchesTitle = <QualificationTitle>משחקי מוקדמות:</QualificationTitle>;
    qualificationMatchesViews = qualificationMatches.map(match => (
      <Match key={match.team1.player1.name + match.team2.player1.name} {...match} />
    ));
  }

  return (
    <PoolContainer>
      <PoolTable teamsNum={teams.length}>
        <Title>בית {number}</Title>
        <RankTitle>נק' דירוג</RankTitle>
        <PlayersTitle>קבוצה</PlayersTitle>
          {teamViews}
    </PoolTable>
      {qualMatchesTitle}
      {qualificationMatchesViews}
    </PoolContainer>
  );
};

const PoolContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: flex-start;

  margin: 1rem;
`;

const Title = styled.div`
    grid-column: 1/span 2;
    grid-row: 1/2;

    align-self: stretch;
    justify-self: stretch;

    background-color: #555;
    color: #fff;

    text-align: center;
`

const PoolTable = styled.div`
  display: grid;
  grid-template-columns: 5.5rem 14rem;
  grid-template-rows: auto auto repeat(${props => props.teamsNum * 2}, auto);

  justify-items: start;
  align-items: center;

  border: 1px solid black;
  margin-bottom: 1rem;

  & > * {
    padding: 0.5rem;
  }
`;

const TableTitle = styled.p`
    font-weight: 600;
    grid-row: 2/3;
`

const RankTitle = styled(TableTitle)`
    grid-column: 1/2;
`

const PlayersTitle = styled(TableTitle)`
    grid-column: 2/3;
`

const Rank = styled.div`
  grid-column: 1/2;
  grid-row: ${props => props.row} / span 2;
`;

const TeamBackground = styled.div`
  grid-column: 1 / span 2;
  grid-row: ${props => props.row} / span 2;
  background-color: ${props => props.teamIndex % 2 === 0 ? "#f6f5f5" : "#fff"};
  justify-self: stretch;
  align-self: stretch;
`;

const Player = styled.div`
  grid-column: 2/3;
  grid-row: ${props => props.row} / span 1;
`;

const QualificationTitle = styled.p`
    margin-bottom: 1rem;
`

export default Pool;
