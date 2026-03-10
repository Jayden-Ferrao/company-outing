using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CompanyOuting.App.Endpoints;

public static class ExperienceEndpoints
{
    public static void MapExperienceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/experiences");

        group.MapGet("/", () => 
        {
            return Results.Ok(Experiences);
        });

        group.MapGet("/{slug}", (string slug) => 
        {
            var exp = Experiences.FirstOrDefault(x => x.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
            return exp is not null ? Results.Ok(exp) : Results.NotFound();
        });
    }

    private static readonly List<ExperienceDetail> Experiences =
    [
        new(
            "culinary-team-experience",
            "Culinary Team Experience",
            "Italy",
            "Tuscany, Italy",
            "15-80",
            "3 days, 2 nights",
            4.9m,
            201,
            980,
            15,
            80,
            "Culinary Connections",
            "https://images.unsplash.com/photo-1545239351-1141bd82e8a6?q=80&w=120&auto=format&fit=crop",
            "https://images.unsplash.com/photo-1556910103-1c02745aae4d?q=80&w=1400&auto=format&fit=crop",
            [
                "https://images.unsplash.com/photo-1556910103-1c02745aae4d?q=80&w=300&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1517248135467-4c7edcad34c4?q=80&w=300&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1414235077428-338989a2e8c0?q=80&w=300&auto=format&fit=crop"
            ],
            ["Culinary", "Cultural", "Team Building", "Spring", "Summer", "Fall"],
            "Bring your team together through the art of cooking. Learn from Michelin-star chefs in Tuscany while enjoying wine tasting and Italian culture.",
            "Best for 15-80 person teams",
            [
                "Day 1: Arrival, welcome dinner, and culinary introduction",
                "Day 2: Cooking masterclass, wine tasting, team cooking challenge",
                "Day 3: Market visit, final meal preparation, and farewell"
            ],
            [
                "Accommodation in villa",
                "All meals and wine",
                "Chef instruction",
                "Cooking equipment",
                "Recipe books"
            ],
            [
                new("Sweet Meetings at Madame Chocola", "Stavanger", 4.9m, 850, "https://images.unsplash.com/photo-1519864600265-abb23847ef2c?q=80&w=400&auto=format&fit=crop"),
                new("Pimp My Party at Gaffel & Karaffel", "Stavanger", 4.7m, 950, "https://images.unsplash.com/photo-1530103862676-de8c9debad1d?q=80&w=400&auto=format&fit=crop"),
                new("Progressive Dinner Party at Supper Club Stavanger", "Stavanger", 4.9m, 1250, "https://images.unsplash.com/photo-1414235077428-338989a2e8c0?q=80&w=400&auto=format&fit=crop")
            ]
        ),
        new(
            "wellness-mindfulness-retreat",
            "Wellness & Mindfulness Retreat",
            "Indonesia",
            "Ubud, Bali",
            "10-50",
            "4 days, 3 nights",
            4.9m,
            167,
            920,
            10,
            50,
            "Serenity Collective",
            "https://images.unsplash.com/photo-1494790108377-be9c29b29330?q=80&w=120&auto=format&fit=crop",
            "https://images.unsplash.com/photo-1544367567-0f2fcb009e0b?q=80&w=1400&auto=format&fit=crop",
            [
                "https://images.unsplash.com/photo-1544367567-0f2fcb009e0b?q=80&w=300&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1506126613408-eca07ce68773?q=80&w=300&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1518611012118-696072aa579a?q=80&w=300&auto=format&fit=crop"
            ],
            ["Wellness", "Mindfulness", "All Year"],
            "Restore focus and team energy with guided yoga, mindfulness sessions, and curated wellness experiences in Bali.",
            "Best for 10-50 person teams",
            [
                "Day 1: Arrival, sunset meditation, healthy welcome dinner",
                "Day 2: Sunrise yoga, breathwork, leadership reflection workshop",
                "Day 3: Spa recovery, nature walk, closing circle",
                "Day 4: Departure brunch and transfers"
            ],
            [
                "Boutique villa stay",
                "Daily yoga sessions",
                "Healthy meal plan",
                "Spa treatment",
                "Facilitator-led workshops"
            ],
            [
                new("Tech Summit & Beach Retreat", "Bali", 4.7m, 1250, "https://images.unsplash.com/photo-1469796466635-455ede028aca?q=80&w=400&auto=format&fit=crop"),
                new("Mindful Leadership Escape", "Lombok", 4.8m, 980, "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee?q=80&w=400&auto=format&fit=crop"),
                new("Sunrise Strategy Camp", "Ubud", 4.6m, 870, "https://images.unsplash.com/photo-1472396961693-142e6e269027?q=80&w=400&auto=format&fit=crop")
            ]
        )
    ];

    public sealed record ExperienceDetail(
        string Slug,
        string Title,
        string Country,
        string Location,
        string People,
        string Duration,
        decimal Rating,
        int Reviews,
        int Price,
        int MinGuests,
        int MaxGuests,
        string ProviderName,
        string ProviderAvatar,
        string HeroImage,
        List<string> GalleryImages,
        List<string> Tags,
        string About,
        string TeamFit,
        List<string> Itinerary,
        List<string> Included,
        List<SimilarExperience> Similar
    );

    public sealed record SimilarExperience(string Title, string Location, decimal Rating, int Price, string Image);
}
