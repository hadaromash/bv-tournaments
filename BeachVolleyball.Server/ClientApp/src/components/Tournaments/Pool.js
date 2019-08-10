import React, { useState, useRef } from "react";
import styled from "styled-components";
import Table from "react-bootstrap/Table";
import { Callout, DirectionalHint  } from "office-ui-fabric-react/lib/Callout";
import PlayerCard from "../PlayerCard";

const Pool = ({ number, teams, qualificationMatches }) => {
  const PlayerView = props => {
    const [isCardOpen, setCardOpen] = useState(false);
    const playerRef = useRef(null);
    return (
      <React.Fragment>
        <div ref={playerRef}>
          <PlayerLink onClick={() => setCardOpen(!isCardOpen)}>{props.name}</PlayerLink>
        </div>
        {isCardOpen && (
          <Callout
            onDismiss={() => setCardOpen(false)}
            target={playerRef}
            directionalHint={DirectionalHint.rightBottomEdge}
            coverTarget={true}
            isBeakVisible={false}
            gapSpace={0}
          >
            <PlayerCard {...props} />
          </Callout>
        )}
      </React.Fragment>
    );
  };

  const TeamView = ({ player1, player2 }) => (
    <React.Fragment>
      <PlayerView style={{ marginBottom: "0.4rem" }} {...player1} />
      <PlayerView {...player2} />
    </React.Fragment>
  );

  const teamViews = teams.map((team, index) => (
    <tr key={team.player1.name + team.player2.name}>
      <td>{index + 1}</td>
      <td>
        <PlayersContainer>
          <TeamView {...team} />
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
            <TeamView {...match.team1} />
            <hr style={{ width: "80%" }} />
            <TeamView {...match.team2} />
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
            <th style={{ backgroundColor: "#555", color: "#fff", textAlign: "center"}} colSpan="3">
              בית {number}
            </th>
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

const PlayerLink = styled.p`
  cursor: pointer;
`

export default Pool;
