import React, { useContext } from "react";
import { TournamentsContext } from "../../Tournaments.context";
import styled from "styled-components";
import { Nav } from "reactstrap";
import Spinner from "react-bootstrap/Spinner";
import { TournamentLink } from "../Tournaments/TourLink";
import Logo from "./Logo";
import Madbiron from "./madbiron-banner.png";
import Media from "../../utils/Media";

const Home = () => {
  const { tournamentsState } = useContext(TournamentsContext);
  return (
    <HomeContainer>
      <Logo />
      <Intro />
      {!tournamentsState.loading && !tournamentsState.error && (
        <TournamentsNav />
      )}
      {tournamentsState.loading && <Loading />}
      {tournamentsState.error && (
        <p>אוף! לא הצלחתי למשוך את המידע מהשרת :( נסה\י לרענן את העמוד</p>
      )}
      <Banner src={Madbiron} alt="ארז אמברצ'י המדבירון הירוק" />
    </HomeContainer>
  );
};

const Intro = () => (
  <p>
    האתר מיועד למשתתפי הטורנירים בסבב הישראלי בכדורעף החופים שלא יכולים לחכות
    לפרסום הבתים על-ידי האיגוד.
    <br />
    <br />
    אבל רגע, איך זה עובד?
    <br />
    טוב ששאלתם. האתר מושך מאתר איגוד הכדורעף את רשימת השחקנים שנרשמו לטורניר בכל
    קטגוריה, ומחשב לפי נקודות הדירוג מה יהיו הבתים. שיבוץ הבתים מתבצע על פי שיטת
    "הנחש" הידועה שבה עושה שימוש איגוד הכדורעף.
    <br />
    <br />
    רוצים לשלוח פידבק, הצעה או סתם מילה טובה?{" "}
    <a href="mailto:hadarom13@gmail.com">שלחו מייל</a>.
  </p>
);

const Loading = () => (
  <div>
    <p>עוד רגע המידע יטען...</p>
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
      <p>יאללה, בואו נתחיל!</p>
      <Nav vertical style={{ alignSelf: "center" }}>
        {tourLinks}
      </Nav>
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

const Banner = styled.img`
  max-width: 100%;
  height: auto;
  align-self: center;
  margin-top: 2rem;

  ${Media.tablet`
    max-width: 70%;
  `}

  ${Media.desktop`
    max-width: 100%;
  `}
`;
