import React from "react";
import styled from "styled-components";
import Pool from "./Pool";

const Pools = ({ pools }) => {
  const poolsViews = pools.map(pool => <Pool {...pool} />);
  return <PoolsContainer>{poolsViews}</PoolsContainer>;
};

const PoolsContainer = styled.div`
  display: flex;
  flex-direction: column;
`;

export default Pools;