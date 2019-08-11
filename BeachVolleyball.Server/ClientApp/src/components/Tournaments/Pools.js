import React from "react";
import styled from "styled-components";
import Pool from "./Pool";
import media from "../../utils/Media";
import Share from "../Whatsapp/WhatsappShare";
import WALogo from "../Whatsapp/logo.png";

const Pools = ({ pools, teamsNumber }) => {
  if (pools.length > 0) {
    const poolsViews = pools.map(pool => <Pool key={pool.number} {...pool} />);
    return (
      <CategoryContainer>
        <p>
          מספר הקבוצות הרשומות בקטגוריה: {teamsNumber}
        </p>
        <Share text={window.location.href}><p>שתפו בוואטסאפ <img src={WALogo} height="24px" width="auto" alt="Whatsapp icon"/></p></Share>
        <PoolsContainer>{poolsViews}</PoolsContainer>
      </CategoryContainer>
    );
  } else {
    return (
      <PoolsContainer>
        <p>
          כמה עצוב, אף זוג עדיין לא נרשם לקטגוריה זו{" "}
          <span role="img" aria-label="sad emoji" style={{ margin: "0px" }}>
            &#128577;
          </span>
        </p>
      </PoolsContainer>
    );
  }
};

const CategoryContainer = styled.div`
  display: flex;
  flex-direction: column;
  & > * {
    margin-top: 1rem;
  }
`

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

export default Pools;
