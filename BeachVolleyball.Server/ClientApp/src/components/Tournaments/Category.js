import React, { useCallback } from "react";
import styled from "styled-components";
import Pool from "./Pool";
import media from "../../utils/Media";
import Share from "../Whatsapp/WhatsappShare";
import WALogo from "../Whatsapp/logo.png";
import { AppInsights } from "applicationinsights-js";
import NewTabLink from "../NewTabLink";

const Category = ({ pools, teamsNumber, displayName, webPage }) => {
  const LogShareEvent = useCallback(() => {
    console.log("Logging share event");
    AppInsights.trackEvent("CategoryWhatsappShare", {
      url: window.location.href,
      category: displayName
    });
  });

  const poolsViews = pools.map(pool => <Pool key={pool.number} {...pool} />);
  return (
    <CategoryContainer>
      <p>מספר הקבוצות הרשומות בקטגוריה: {teamsNumber}</p>
      <div><NewTabLink href={webPage + "#ranking"}>דף הקטגוריה באתר איגוד הכדורעף</NewTabLink></div>
      <div>
        <Share text={window.location.href} handleClick={LogShareEvent}>
          שתפו בוואטסאפ{" "}
          <img src={WALogo} height="24px" width="auto" alt="Whatsapp icon" />
        </Share>
      </div>
      <PoolsContainer>{poolsViews}</PoolsContainer>
    </CategoryContainer>
  );
};

const CategoryContainer = styled.div`
  display: flex;
  flex-direction: column;
  & > * {
    margin-top: 1rem;
  }
`;

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

export default Category;
