# startup-alerter

Purpose: Send an sms message each time the application is run.

Usage: Intended to be run as a Windows scheduled task on startup.

Configuration:
* A [twilio](https://www.twilio.com/)  account is required
* The app.config file must be updated with your:
  * twilio application SID
  * twillio authorisation token
  * twilio phone number
  * target phone number
