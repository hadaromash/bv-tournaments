import React from "react";
import styled from "styled-components";
import Pool from "./Pool";

const Pools = ({ pools }) => {
  if (pools.length > 0) {
    const poolsViews = pools.map(pool => <Pool key={pool.number} {...pool} />);
    return <PoolsContainer>{poolsViews}</PoolsContainer>;
  }
  else {
    return <PoolsContainer>כמה עצוב, אף זוג עדיין לא נרשם לקטגוריה זו</PoolsContainer>;
  }
};

const PoolsContainer = styled.div`
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: start;

  margin: 30px;
`;

export default Pools;