import React from "react";
import styled from "styled-components";
import Team from "./Team";

const Match = ({ team1, team2 }) => (
  <MatchContainer>
    <Team {...team1} />
    <VsText>Vs.</VsText>
    <Team {...team2} />
  </MatchContainer>
);

const MatchContainer = styled.div`
  display: flex;
  flex-direction: row;
`;

const VsText = styled.p`
  font-size: 1.2rem;
`;

export default Match;