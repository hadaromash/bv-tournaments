import React, { useContext } from "react";
import { TournamentsContext } from "../Tournaments.context";
import styled from "styled-components";
import { Nav, NavItem, NavLink } from "reactstrap";
import { Link } from "react-router-dom";
import Spinner from "react-bootstrap/Spinner";

const Home = () => {
  const { tournamentsState } = useContext(TournamentsContext);
  return (
    <HomeContainer>
      <p>
        ברוכ\ה הבא\ה! רוצה לדעת נגד מי תשחק\י בבית בטורניר? בחר\י בטורניר
        מהרשימה:
      </p>
      <TournamentsNav />
      {tournamentsState.loading && <Loading />}
      {tournamentsState.error && (
        <p>אוף! לא הצלחתי למשוך את המידע מהשרת :( נסה\י לרענן את העמוד</p>
      )}
    </HomeContainer>
  );
};

const Loading = () => <Spinner animation="grow" variant="info" />;

const TournamentsNav = () => {
  const { tournamentsState } = useContext(TournamentsContext);
  if (tournamentsState.tournaments.length === 0) {
    return null;
  }

  console.log(
    "Creating tournament links: " + tournamentsState.tournaments.length
  );
  const tourLinks = tournamentsState.tournaments.map(tour => (
    <NavItem key={tour.id}>
      <NavLink tag={Link} to={"/tournaments/" + tour.id}>
        {tour.name}
      </NavLink>
    </NavItem>
  ));

  return <Nav vertical>{tourLinks}</Nav>;
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
