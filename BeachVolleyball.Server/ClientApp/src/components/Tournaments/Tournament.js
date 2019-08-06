import React from "react";
import Tabs from "react-bootstrap/Tabs";
import Tab from "react-bootstrap/Tab";
import Pools from './Pools';

const Tournament = ({ name, categories }) => {
  const categoryTabs = categories.map((category, index) => (
    <Tab
      key={category.displayName}
      title={category.displayName}
      eventKey={category.displayName}
    >
        <Pools {...category}/>
    </Tab>
  ));

  return (
    <Tabs>
      {categoryTabs}
    </Tabs>
  );
};

export default Tournament;
