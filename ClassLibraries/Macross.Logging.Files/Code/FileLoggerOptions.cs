﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Collections.Generic;

using Microsoft.Extensions.Hosting;

namespace Macross.Logging.Files
{
	/// <summary>
	/// Stores options for the <see cref="FileLogger"/>.
	/// </summary>
	public class FileLoggerOptions
	{
		/// <summary>
		/// Gets the default <see cref="LoggerGroupOptions"/> filters used to group log messages by category.
		/// </summary>
		/// <remarks>
		/// Default settings are constructed as:
		/// <code><![CDATA[
		///   new LoggerGroupOptions[]
		///   {
		///   	new LoggerGroupOptions
		///   	{
		///   		GroupName = "System",
		///   		CategoryNameFilters = new string[] { "System*" }
		///   	},
		///   	new LoggerGroupOptions
		///   	{
		///   		GroupName = "Microsoft",
		///   		CategoryNameFilters = new string[] { "Microsoft*" }
		///   	},
		///   };
		/// ]]></code>
		/// </remarks>
		public static IEnumerable<LoggerGroupOptions> DefaultGroupOptions { get; } = new LoggerGroupOptions[]
		{
			new LoggerGroupOptions
			{
				GroupName = "System",
				CategoryNameFilters = new string[] { "System*" }
			},
			new LoggerGroupOptions
			{
				GroupName = "Microsoft",
				CategoryNameFilters = new string[] { "Microsoft*" }
			},
		};

		/// <summary>
		/// Gets the default <see cref="JsonSerializerOptions"/> options to use when serializing messages.</summary>
		/// <remarks>
		/// Default settings are constructed as:
		/// <code><![CDATA[
		///   new JsonSerializerOptions
		///   {
		///       IgnoreNullValues = true,
		///       Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
		///   };
		/// ]]></code>
		/// </remarks>
		public static JsonSerializerOptions DefaultJsonOptions { get; } = new JsonSerializerOptions
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
		};

		/// <summary>
		/// Gets the default log file name pattern, used when <see cref="LogFileNamePattern"/> is not specified and <see cref="IncludeGroupNameInFileName"/> is false.
		/// </summary>
		public const string DefaultLogFileNamePattern = "{MachineName}.{DateTime:yyyyMMdd}.log";

		/// <summary>
		/// Gets the default log file name pattern, used when <see cref="LogFileNamePattern"/> is not specified and <see cref="IncludeGroupNameInFileName"/> is true.
		/// </summary>
		public const string DefaultGroupLogFileNamePattern = "{MachineName}.{GroupName}.{DateTime:yyyyMMdd}.log";

		/// <summary>
		/// Gets the default log file directory, used when <see cref="LogFileDirectory"/> is not specified.
		/// </summary>
		public const string DefaultLogFileDirectory = "C:\\Logs\\{ApplicationName}\\";

		/// <summary>
		/// Gets the default log file archive directory, used when <see cref="LogFileArchiveDirectory"/> is not specified.
		/// </summary>
		public const string DefaultLogFileArchiveDirectory = "C:\\Logs\\Archive\\{ApplicationName}\\";

		/// <summary>
		/// Gets the default log file cutover time, used when <see cref="LogFileCutoverTime"/> is not specified.
		/// </summary>
		public static TimeSpan DefaultLogFileCutoverTime { get; } = new TimeSpan(0, 0, 0);

		/// <summary>
		/// Gets the default log file archive time, used when <see cref="LogFileArchiveTime"/> is not specified.
		/// </summary>
		public static TimeSpan DefaultLogFileArchiveTime { get; } = new TimeSpan(1, 0, 0);

		/// <summary>
		/// Gets or sets the application name string that should be used as the {ApplicationName} token in file paths. If not supplied the <see cref="IHostEnvironment.ApplicationName"/> value will be used.
		/// </summary>
		public string? ApplicationName { get; set; }

		/// <summary>
		/// Gets or sets the directory used to store log files. Default value: <see cref="DefaultLogFileDirectory"/>.
		/// </summary>
		public string? LogFileDirectory { get; set; }

		/// <summary>
		/// Gets or sets the directory used to store archived log files. Default value: <see cref="DefaultLogFileArchiveDirectory"/>.
		/// </summary>
		public string? LogFileArchiveDirectory { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether or not group name should be part of the log file name. Default value: false.
		/// </summary>
		public bool IncludeGroupNameInFileName { get; set; }

		/// <summary>
		/// Gets or sets the maximum file size in kilobytes of log files. Use 0 to indicate no maxium size. Default value: 20 Mb.
		/// </summary>
		public int LogFileMaxSizeInKilobytes { get; set; } = 1024 * 20; // 20 Mb default file size.

		/// <summary>
		/// Gets or sets the log file naming pattern to use. Default value: Either <see cref="DefaultLogFileNamePattern"/> or <see cref="DefaultGroupLogFileNamePattern"/>.
		/// </summary>
		public string? LogFileNamePattern { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether a disk test should be performed on startup. Default value: true.
		/// </summary>
		public bool TestDiskOnStartup { get; set; } = true;

		/// <summary>
		/// Gets or sets a value indicating whether old log files should be archived on startup. Default value: true.
		/// </summary>
		public bool ArchiveLogFilesOnStartup { get; set; } = true;

		/// <summary>
		/// Gets or sets a value indicating what time zone mode should be used for cutover and archive time calculations. Valid values: Utc or Local. Default value: Local.
		/// </summary>
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public DateTimeKind CutoverAndArchiveTimeZoneMode { get; set; } = DateTimeKind.Local;

		/// <summary>
		/// Gets or sets the time of day to cutover log files. Default value: <see cref="DefaultLogFileCutoverTime"/>.
		/// </summary>
		public TimeSpan? LogFileCutoverTime { get; set; }

		/// <summary>
		/// Gets or sets the time of day to archive log files. Default value: <see cref="DefaultLogFileArchiveTime"/>.
		/// </summary>
		public TimeSpan? LogFileArchiveTime { get; set; }

		/// <summary>
		/// Gets or sets the filters to use to group log messages by category.
		/// </summary>
		/// <remarks>
		/// See <see cref="DefaultGroupOptions"/> for default values.
		/// </remarks>
		public IEnumerable<LoggerGroupOptions>? GroupOptions { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="JsonSerializerOptions"/> to use when serializing messages.
		/// </summary>
		/// <remarks>
		/// See <see cref="DefaultJsonOptions"/> for default values.
		/// </remarks>
		public JsonSerializerOptions? JsonOptions { get; set; }
	}
}
