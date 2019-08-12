import React from "react";
import Tabs from "react-bootstrap/Tabs";
import Tab from "react-bootstrap/Tab";
import Category from "./Category";
import history from "../../History";
import { createTournamentPath } from "./TourLink";
import styled from "styled-components";
import NewTabLink from "../NewTabLink";

const Tournament = props => {
  const handleTabChange = key => {
    history.push(createTournamentPath(props.id, key));
  };

  const categoryTabs = props.categories.map(category => (
    <Tab key={category.id} title={category.displayName} eventKey={category.id}>
      <Category {...category} />
    </Tab>
  ));

  return (
    <div>
      <Title>{props.name} - <NewTabLink href={props.webPage}>דף הטורניר באתר איגוד הכדורעף</NewTabLink></Title>
      <Tabs
        variant="pills"
        activeKey={props.match.params.categoryId}
        onSelect={k => handleTabChange(k)}
      >
        {categoryTabs}
      </Tabs>
    </div>
  );
};

export default Tournament;

const Title = styled.p`
  font-weight: 600;
  margin: 2rem 0rem;
`;
