namespace Microsoft.Extensions.Hosting;

using Cirreum.Logging.Deferred;
using Cirreum.Providers;
using Cirreum.SecretsProvider;
using Cirreum.SecretsProvider.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class HostApplicationBuilderExtensions {

	/// <summary>
	/// Register Secrets Provider instances.
	/// </summary>
	/// <typeparam name="TRegistrar"></typeparam>
	/// <typeparam name="TSettings"></typeparam>
	/// <typeparam name="TInstanceSettings"></typeparam>
	/// <param name="builder"><see cref="IHostApplicationBuilder"/></param>
	/// <returns><see cref="IHostApplicationBuilder"/></returns>
	public static IHostApplicationBuilder RegisterSecretsProvider<TRegistrar, TSettings, TInstanceSettings>(
		this IHostApplicationBuilder builder)
		where TRegistrar : SecretsProviderRegistrar<TSettings, TInstanceSettings>, new()
		where TSettings : SecretsProviderSettings<TInstanceSettings>
		where TInstanceSettings : SecretsProviderInstanceSettings {

		var registrarName = typeof(TRegistrar).Name;
		var deferredLogger = Logger.CreateDeferredLogger();

		using (var loggingScope = deferredLogger.BeginScope($"Registrar {registrarName}")) {

			// Check if this specific registrar type is already registered
			if (builder.Services.IsMarkerTypeRegistered<TRegistrar>()) {
				deferredLogger.LogDebug($"Duplicate request for '{registrarName}' and will be skipped.");
				return builder;
			}
			// Mark this registrar type as registered
			builder.Services.MarkTypeAsRegistered<TRegistrar>();

			var registrar = new TRegistrar();

			var providerSectionKey = GetProviderConfigPath(registrar.ProviderType, registrar.ProviderName);
			var providerSection = builder.Configuration.GetSection(providerSectionKey);
			if (!providerSection.Exists()) {
				deferredLogger.LogWarning($"No configuration settings found for '{registrarName}'.");
				return builder;
			}

			var providerSettings =
				providerSection.Get<TSettings>()
				?? throw new Exception(
					$"Invalid configuration for '{registrarName}' - section exists but cannot be bound to settings.");

			if (providerSettings.Instances.Count == 0) {
				deferredLogger.LogWarning($"0 instances found to register for '{registrarName}'.");
				return builder;
			}

			// Register the secrets Provider
			registrar.Register(
				providerSettings,
				builder.Services,
				builder.Configuration);

			deferredLogger.LogDebug(
				$"Registered {providerSettings.Instances.Count} provider instances for '{registrarName}' of type '{registrar.ProviderType}'.");

		}

		return builder;

	}

	// Helper method for building provider configuration paths
	private static string GetProviderConfigPath(ProviderType providerType, string providerName) =>
		$"Cirreum:{providerType}:Providers:{providerName}";

}