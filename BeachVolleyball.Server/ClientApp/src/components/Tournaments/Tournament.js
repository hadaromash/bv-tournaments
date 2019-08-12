import React from "react";
import Tabs from "react-bootstrap/Tabs";
import Tab from "react-bootstrap/Tab";
import Category from "./Category";
import history from "../../History";
import { createTournamentPath } from "./TourLink";
import styled from "styled-components";
import NewTabLink from "../NewTabLink";
import { Container } from "reactstrap";

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
      <TitleSection>
        <Title>
          {props.name}<br/>
          <NewTabLink href={props.webPage}>
            דף הטורניר באתר איגוד הכדורעף
          </NewTabLink>
        </Title>
      </TitleSection>
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
  padding: 2rem 2rem;
  text-align: center;
`;

const TitleSection = styled.section`
  background-color: rgba(0, 0, 0, 0.05);

  width: 100vw;
  position: relative;
  margin-right: -50vw;
  right: 50%;

  margin-bottom: 1rem;
`;
