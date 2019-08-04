import React from "react";
import styled from "styled-components";
import Pool from "./TablePool";

const Pools = ({ pools }) => {
  const poolsViews = pools.map(pool => <Pool key={pool.number} {...pool} />);
  return <PoolsContainer>{poolsViews}</PoolsContainer>;
};

const PoolsContainer = styled.div`
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: start;

`;

export default Pools;