import React from "react";
import styled from "styled-components";
import Team from "./Team";

const Match = ({ team1, team2 }) => (
  <MatchContainer>
    <Team {...team1} />
    <VsText>נגד</VsText>
    <Team {...team2} />
  </MatchContainer>
);

const MatchContainer = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;

  & > * {
    margin-left: 1rem;
  }
`;

const VsText = styled.p`
  font-size: 1.2rem;
`;

export default Match;