import React from "react";
import styled from "styled-components";

const Player = ({ name, rank, previousYearRank, association, age }) => (
  <PlayerContainer>
    <NameTitle>Name: {name}</NameTitle>
    <RankTitle>Rank points: {rank}</RankTitle>
    <PrevYearRankTitle>Previous year rank points: {previousYearRank}</PrevYearRankTitle>
    <AssociationTitle>Association: {association}</AssociationTitle>
    <AgeTitle>Age: {age}</AgeTitle>
  </PlayerContainer>
);

const PlayerContainer = styled.div`
  display: flex;
  flex-direction: column;
  & > * {
      margin-bottom: 5px;
  }
`;

const Title = styled.p`
  grid-row: 1/2;
  font-size: 0.8rem;
`;

const NameTitle = styled(Title)`
  grid-column: 1/2;
`;

const RankTitle = styled(Title)`
  grid-column: 2/3;
`;

const PrevYearRankTitle = styled(Title)`
  grid-column: 3/4;
`;

const AssociationTitle = styled(Title)`
  grid-column: 4/5;
`;

const AgeTitle = styled(Title)`
  grid-column: 5/6;
`;

const Data = styled.p`
  grid-row: 2/3;
  font-size: 0.8rem;
`;

const Name = styled(Data)`
  grid-column: 1/2;
`;

const Rank = styled(Data)`
  grid-column: 2/3;
`;

const PrevYearRank = styled(Data)`
  grid-column: 3/4;
`;

const Association = styled(Data)`
  grid-column: 4/5;
`;

const Age = styled(Data)`
  grid-column: 5/6;
`;

export default Player;
