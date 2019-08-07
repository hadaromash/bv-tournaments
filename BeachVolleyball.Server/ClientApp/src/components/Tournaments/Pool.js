import React from "react";
import styled from "styled-components";
import Table from "react-bootstrap/Table";

const Pool = ({ number, teams, qualificationMatches }) => {
  const teamViews = teams.map((team, index) => (
    <tr key={team.player1.name + team.player2.name}>
      <td>{index + 1}</td>
      <td>
        <PlayersContainer>
          <p style={{ marginBottom: "0.4rem" }}>{team.player1.name}</p>
          <p>{team.player2.name}</p>
        </PlayersContainer>
      </td>
      <td>{team.rank}</td>
    </tr>
  ));

  let qualificationMatchesViews = null;
  if (qualificationMatches && qualificationMatches.length > 0) {
    qualificationMatchesViews = qualificationMatches.map((match, index) => (
      <tr key={match.team1.player1.name + match.team2.player1.name}>
        <td>{teams.length + index + 1}</td>
        <td>
          <PlayersContainer>
            <p style={{ marginBottom: "0.4rem" }}>{match.team1.player1.name}</p>
            <p>{match.team1.player2.name}</p>
            <hr style={{ width: "80%" }} />
            <p style={{ marginBottom: "0.4rem" }}>{match.team2.player1.name}</p>
            <p>{match.team2.player2.name}</p>
          </PlayersContainer>
        </td>
        <td>מוקדמות</td>
      </tr>
    ));
  }

  return (
    <PoolContainer>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th style={{backgroundColor: "#555", color: "#fff"}}  colSpan="3">בית {number}</th>
          </tr>
          <tr>
            <th>#</th>
            <th>קבוצה</th>
            <th>נק' דירוג</th>
          </tr>
        </thead>
        <tbody>
          {teamViews}
          {qualificationMatchesViews}
        </tbody>
      </Table>
    </PoolContainer>
  );
};

const PlayersContainer = styled.div`
  display: flex;
  flex-direction: column;
  justify-items: start;
`;

const PoolContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: flex-start;

  margin: 0.5rem;
`;

const PoolTitle = styled.p`
  font-weight: 600;
  margin-bottom: 0.5rem;
`;

export default Pool;
