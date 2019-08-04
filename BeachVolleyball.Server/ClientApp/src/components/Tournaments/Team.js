import React from "react";
import styled from "styled-components";
import Player from "./Player";

const Team = ({ player1, player2, rank, previousYearRank }) => (
  <TeamContainer>
    <Player1 {...player1} />
    <Player2 {...player2} />
    <Rank>{rank}</Rank>
  </TeamContainer>
);

const TeamContainer = styled.div`
  display: grid;
  grid-template-rows: repeat(2, auto);
  grid-template-columns: 30px auto;
  justify-items: start;

  & > * {
      margin-bottom: 10px;
  }
`;
const Player1 = styled(Player)`
  grid-row: 1/2;
  grid-column: 1/2;
`;

const Player2 = styled(Player)`
  grid-row: 2/3;
  grid-column: 1/2;
`;

const Rank = styled.p`
  grid-row: 1/3;
  grid-column: 1/2;

  align-self: center;
`;

export default Team;
