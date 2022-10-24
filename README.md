# BeatSaverSharper

A C# library to interact with the [BeatSaver](https://beatsaver.com) [API](https://api.beatsaver.com/docs).

## ⬇️ Downloading

If you're trying to download this as a dependency for a Beat Saber mod, go to the [Releases](https://github.com/Auros/BeatSaverSharper/releases/latest) page, find and download the latest (or the one that you need) `BeatSaverSharper-Beat Saber.zip`, download it, and extract it in your `Beat Saber` folder.

Alternatively, you can pick up the latest successful build based off of `main` in the [GitHub Actions](https://github.com/Auros/BeatSaverSharper/actions) page as an artifact (both normal and Unity versions).

## 🌱 Origin

This project is a replacement/continuation of BeatSaverSharp, the official .NET library for interacting with the BeatSaver API made by [lolPants](https://github.com/lolPants). It has since been taken down since it was no longer compatible with the new [BeatSaver](https://github.com/beatmaps-io/beatsaver-main) made by [Top_Cat](https://github.com/Top-Cat).

## 🛠️ Building

Building requires at least the .NET 6 SDK. The main project is .NET Standard 2.0, however the tests use .NET 6.

Pull from git or download this repository.

If on CLI, you can run

```
dotnet restore
```
to restore all of the project dependencies.

Then you will be able to build using 
```
dotnet build
```

## 🧪 Testing

The tests **DO NOT** work with the `Release-Unity` configuration, so make sure you're not building for that release.

In order for the tests to be functional, [BeatSaver](https://beatsaver.com)'s API needs to be online.

If on CLI, you can run the tests using
```
dotnet test
```

## ✈️ Contributing

If you want to contribute, fork this repository and make a new branch on your repository! I don't recommend working directly on your `main` branch because it'll be harder to rebase and fetch if you want to contribute in the future.

Once you've made your changes, create a [Pull Request](https://github.com/Auros/BeatSaverSharper/pulls) and reasonably list the changes you've made in bullet points

Example:
Title - Added exampleProperty to User model
```
Beat Saver got an update, so I've decided to add the new property.

* Added a new property `exampleProperty` to the `User` model since it was recently added to BeatSaver's API
```

Hopefully the tests pass, and I look at it. Feel free to message me (Auros#0001) on Discord right afterwards about it too, and continually remind me if I don't look at it.

## 🤡 BeatSaverSharper?
I hardly know her!
