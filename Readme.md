# Tyrrrz.AspNetCore.Mvc.Lightbox

Tag helper used to enable [Lightbox](http://lokeshdhakar.com/projects/lightbox2/) in ASP.net Core MVC views.

## Download

- Using nuget: `Install-Package Tyrrrz.AspNetCore.Mvc.Lightbox`

## Usage

Make the tag helper available with the `addTagHelper` directive either in your view or `_ViewImports.cshtml`.

```
@addTagHelper *, Tyrrrz.AspNetCore.Mvc.Lightbox
```

Put the tag helper at the end of the `body` to initialize Ligthbox.

```html
<lightbox />
```

Mark the image links with `data-lightbox="group-name"` as specified in the Lightbox documentation.

```html
<a href="image.png" data-lightbox="gallery">
	<img src="image.png" />
</a>
```

You can override default settings using attributes. The attribute names match the option names specified in the documentation.

```html
<lightbox image-fade-duration="TimeSpan.FromSeconds(0.4)" max-width="500" wrap-around="true" />
```