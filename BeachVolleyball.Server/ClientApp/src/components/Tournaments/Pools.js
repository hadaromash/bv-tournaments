import React from "react";
import styled from "styled-components";
import Pool from "./Pool";
import media from "../../utils/Media";

const Pools = ({ pools, teamsNumber }) => {
  if (pools.length > 0) {
    const poolsViews = pools.map(pool => <Pool key={pool.number} {...pool} />);
    return <div><CategoryTitle>מספר הקבוצות הרשומות בקטגוריה: {teamsNumber}</CategoryTitle><PoolsContainer>{poolsViews}</PoolsContainer></div>;
  }
  else {
    return <PoolsContainer><p>כמה עצוב, אף זוג עדיין לא נרשם לקטגוריה זו <span role="img" aria-label="sad emoji" style={{margin: "0px"}}>&#128577;</span></p></PoolsContainer>;
  }
};

const PoolsContainer = styled.div`
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: center;

  margin: 30px;
  ${media.desktop`
    & > * {
      margin: 0rem 3rem;
    }
  `}
`;

const CategoryTitle = styled.p`
  margin: 1rem;
`;

export default Pools;