# Conference Manager Example App

This ASP.NET Core 3.0 sample application allows a *conference manager* to register the:

- Room categories
- Rooms
- Conference's subject categories
- Conference's subjects
- Speakers
- Conference's sessions
- Guests
- Time slots

It also allows a *user* to select the sessions he wants to assist to.

## Main technical difficulty

The main technical difficulty with this database is the time slot constraint:

**A time slot has only ONE session in ONE room. It is impossible for two sessions to share the same time slot.**

This constraint is implemented by the composite key in the TimeSlot table
and the many-to-one relationships between TimeSlot and Session/Room.

## The database

The application implements the following Entity-Relationship diagram:

![ER Diagram](./READMEResources/ER_diagram.svg)

## Screenshots

Home page:

![Home page](./READMEResources/home.JPG)

List of room ctaegories:

![Room categories](./READMEResources/room_categories_list.jpg)

List of rooms:

![Rooms](./READMEResources/room_list.jpg)

List of speakers:

![Speakers](./READMEResources/speaker_list.jpg)

List of Subjects:

![Subjects](./READMEResources/subject_list.jpg)

List of time slots:

![Time slots](./READMEResources/timeslots_list.jpg)

Form to add a time slot:

![Time slot form](./READMEResources/timeslots_form.jpg)

List of time slots a user can subscribe to:

![User time slots](./READMEResources/user_timeslots_list.jpg)

Form used by an user to subscribe to a session:

![User time slot form](./READMEResources/user_timeslots_form.jpg)