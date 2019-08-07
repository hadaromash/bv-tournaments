import React from "react";
import Tabs from "react-bootstrap/Tabs";
import Tab from "react-bootstrap/Tab";
import Pools from './Pools';

const Tournament = ({ categories }) => {
  const categoryTabs = categories.map((category) => (
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
