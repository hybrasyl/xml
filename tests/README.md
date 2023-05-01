# Hybrasyl Entity (XML) Unit Tests

Unit tests here are implemented using xUnit.

To run the unit tests, you'll need a checkout of [Ceridwen](https://github.com/hybrasyl/ceridwen), our example Hybrasyl data repository containing items, maps, etc.
The tests are geared towards Ceridwen in terms of total numbers of different files, etc. 

To run the tests, check out Ceridwen, update the `appsettings.json` to point to the checkout, and then run the tests.

# Test Guidelines

**Don't** test built in functionality (like XML deserialization/serialization).
**Do** test additional functionality (anything added via `partial`).
**Do** write useful tests that increase coverage.
**Don't** submit PRs that add additional functionality without adding tests.

