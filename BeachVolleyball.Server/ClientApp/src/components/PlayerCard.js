import React, { useState, useEffect } from "react";
import Card from "react-bootstrap/Card";
import PlayerApi from "../api/PlayerApi";
import DefaultPerson from "./default-person.jpg";

const PlayerCard = ({
  name,
  playerId,
  rank,
  previousYearRank,
  association,
  age
}) => {
  const [photoUrl, setPhotoUrl] = useState(null);
  useEffect(() => {
    const updatePhotoUrl = async () => {
      var api = new PlayerApi();
      try {
        console.log("Getting photo of player id: " + playerId);
        var photoUrl = await api.getPhoto(playerId);
        setPhotoUrl(photoUrl);
      } catch (error) {
        console.error("Failed to get photo");
      }
    };
    updatePhotoUrl();
  }, []);

  const photoSrc = photoUrl ? photoUrl : DefaultPerson;

  return (
    <Card style={{ width: "12rem" }}>
      <Card.Img variant="top" src={photoSrc} />
      <Card.Body>
        <Card.Title>{name}</Card.Title>
        <Card.Text>
          גיל: {age}
          <br />
          אגודה: {association}
          <br />
          נק' דירוג: {rank}
          <br />
          נק' דירוג עונה שעברה: {previousYearRank}
        </Card.Text>
      </Card.Body>
    </Card>
  );
};

export default PlayerCard;
