import React from "react";
import styled from "styled-components";
import Player from "./Player";

const Team = ({ player1, player2, rank, previousYearRank }) => (
  <TeamContainer>
    <Player1 {...player1} />
    <Player2 {...player2} />
    <Rank>Rank: {rank}</Rank>
    <PrevYearRank>Previous year rank: {previousYearRank}</PrevYearRank>
  </TeamContainer>
);

const TeamContainer = styled.div`
  display: grid;
  grid-template-rows: repeat(3, auto);
  grid-template-columns: repeat(2, auto);
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
  grid-row: 1/2;
  grid-column: 2/3;
`;

const Rank = styled.p`
  grid-row: 2/3;
  grid-column: 1 / span 2;
`;

const PrevYearRank = styled.p`
  grid-row: 3/4;
  grid-column: 1 / span 2;
`;

export default Team;
