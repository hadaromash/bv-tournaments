import React, { useContext } from "react";
import { TournamentsContext } from "../Tournaments.context";
import styled from "styled-components";
import { Nav } from "reactstrap";
import Spinner from "react-bootstrap/Spinner";
import { TournamentLink } from "./Tournaments/TourLink";

const Home = () => {
  const { tournamentsState } = useContext(TournamentsContext);
  return (
    <HomeContainer>
      {!tournamentsState.loading && <TournamentsNav />}
      {tournamentsState.loading && <Loading />}
      {tournamentsState.error && (
        <p>אוף! לא הצלחתי למשוך את המידע מהשרת :( נסה\י לרענן את העמוד</p>
      )}
    </HomeContainer>
  );
};

const Loading = () => (
  <div>
    <p>טוען טורנירים...</p>
    <Spinner animation="border" variant="info" />
  </div>
);

const TournamentsNav = () => {
  const { tournamentsState } = useContext(TournamentsContext);
  if (tournamentsState.tournaments.length === 0) {
    return <p>אין אף טורניר בפתח, איזה באסה!</p>;
  }

  console.log(
    "Creating tournament links: " + tournamentsState.tournaments.length
  );
  const tourLinks = tournamentsState.tournaments.map(tour => (
    <TournamentLink {...tour} key={tour.id} />
  ));

  return (
    <React.Fragment>
      <p>
        ברוכ\ה הבא\ה! רוצה לדעת נגד מי תשחק\י בבית בטורניר? בחר\י בטורניר
        מהרשימה:
      </p>
      <Nav vertical>{tourLinks}</Nav>
    </React.Fragment>
  );
};

const HomeContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: start;

  margin-top: 2rem;

  & > * {
    margin-bottom: 30px;
  }
`;

export default Home;
