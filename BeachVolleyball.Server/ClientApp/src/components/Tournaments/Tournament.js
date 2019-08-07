import React from "react";
import Tabs from "react-bootstrap/Tabs";
import Tab from "react-bootstrap/Tab";
import Pools from './Pools';
import history from '../../History';
import { createTournamentPath } from "./TourLink";

const Tournament = (props) => {

  const handleTabChange = (key) => {
    history.push(createTournamentPath(props.id, key));
  }

  const categoryTabs = props.categories.map((category) => (
    <Tab
      key={category.id}
      title={category.displayName}
      eventKey={category.id}
    >
        <Pools {...category}/>
    </Tab>
  ));

  return (
    <Tabs activeKey={props.match.params.categoryId} onSelect={k => handleTabChange(k)}>
      {categoryTabs}
    </Tabs>
  );
};

export default Tournament;
