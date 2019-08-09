import React, { useState, useEffect } from "react";
import Card from "react-bootstrap/Card";

const PlayerCard = ({
  name,
  playerId,
  rank,
  previousYearRank,
  association,
  age
}) => {
  return (
    <Card style={{ width: "18rem" }}>
      <Card.Img variant="top" src="holder.js/100px180" />
      <Card.Body>
        <Card.Title>{name}</Card.Title>
        <Card.Text>
          גיל: {age}<br/>
          אגודה: {association}<br/>
          נק' דירוג: {rank}<br/>
          נק' דירוג עונה שעברה: {previousYearRank}
        </Card.Text>
      </Card.Body>
    </Card>
  );
};


export default PlayerCard;