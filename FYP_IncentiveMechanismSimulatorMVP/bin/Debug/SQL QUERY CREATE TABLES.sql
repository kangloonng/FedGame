CREATE TABLE [dbo].[GameInstance] (
    [Id]                       INT          IDENTITY (1, 1) NOT NULL,
    [Gid]                      VARCHAR (50) NOT NULL,
    [DateTime]                 DATETIME     CONSTRAINT [DF_GameInstance_DateTime] DEFAULT (getdate()) NULL,
    [MarketShare]              FLOAT (53)   NOT NULL,
    [MarketShareFederationPct] FLOAT (53)   NOT NULL,
    [StartingAsset]            FLOAT (53)   NOT NULL,
    [MinTrainingLength]        FLOAT (53)   NOT NULL,
    [MaxTrainingLength]        FLOAT (53)   NOT NULL,
    [MinBidLength]             FLOAT (53)   NOT NULL,
    [MinProfitLength]          FLOAT (53)   NOT NULL,
    [NumFederations]           INT          NOT NULL,
    [NumPlayers]               INT          NOT NULL,
    [MaxTurns]                 INT          NOT NULL,
    [MinDataQuality]           FLOAT (53)   NOT NULL,
    [MaxDataQUality]           FLOAT (53)   NOT NULL,
    [MinDataQuantity]          FLOAT (53)   NOT NULL,
    [MaxDataQuantity]          FLOAT (53)   NOT NULL,
    [DataQualityWeight]        FLOAT (53)   NOT NULL,
    [DataQuantityWeight]       FLOAT (53)   NOT NULL,
    [MinResourceQuantity]      INT          NOT NULL,
    [MaxResourceQuantity]      INT          NOT NULL,
    [InitDataQualityTh]        FLOAT (53)   NOT NULL,
    [InitDataQuantityTh]       FLOAT (53)   NOT NULL,
    [InitResourceQuantityTh]   INT          NOT NULL,
    [InitAmountTh]             INT          NOT NULL,
    CONSTRAINT [PK__tmp_ms_x__3214EC07310EF640] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Participants] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Gid]         INT          NOT NULL,
    [Pid]         INT          NOT NULL,
    [Strategy]    VARCHAR (50) NOT NULL,
    [HumanPlayer] BIT          CONSTRAINT [DF_Participants_HumanPlayer] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK__tmp_ms_x__3214EC07487A469F] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Participants_GameInstance] FOREIGN KEY ([Gid]) REFERENCES [dbo].[GameInstance] ([Id])
);

CREATE TABLE [dbo].[Federations] (
    [Id]                INT          IDENTITY (1, 1) NOT NULL,
    [Gid]               INT          NOT NULL,
    [Fid]               INT          NOT NULL,
    [FederationAsset]   FLOAT (53)   NOT NULL,
    [SchemeAdopted]     VARCHAR (50) NOT NULL,
    [AdmissionPolicyId] INT          NOT NULL,
    CONSTRAINT [PK__Federati__3214EC07F81E90AF] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Federations_GameInstance] FOREIGN KEY ([Gid]) REFERENCES [dbo].[GameInstance] ([Id])
);

CREATE TABLE [dbo].[Bids] (
    [Id]          INT        IDENTITY (1, 1) NOT NULL,
    [Pid]         INT        NOT NULL,
    [Fid]         INT        NOT NULL,
    [AmountBid]   FLOAT (53) NOT NULL,
    [ResourceQty] INT        NOT NULL,
    [DataQty]     FLOAT (53) NOT NULL,
    [DataQuality] FLOAT (53) NOT NULL,
    [Success]     BIT        NOT NULL,
    [Gid]         INT        NOT NULL,
    CONSTRAINT [PK_Bids] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Bids_Participants] FOREIGN KEY ([Pid]) REFERENCES [dbo].[Participants] ([Id]),
    CONSTRAINT [FK_Bids_Federations] FOREIGN KEY ([Fid]) REFERENCES [dbo].[Federations] ([Id])
);

CREATE TABLE [dbo].[InTrainings] (
    [Id]               INT        IDENTITY (1, 1) NOT NULL,
    [Pid]              INT        NOT NULL,
    [Fid]              INT        NOT NULL,
    [Progression]      FLOAT (53) NOT NULL,
    [Turn]             FLOAT (53) NOT NULL,
    [DataQuality]      FLOAT (53) NOT NULL,
    [DataQuantity]     FLOAT (53) NOT NULL,
    [ResourceQuantity] INT        NOT NULL,
    [BidAmount]        FLOAT (53) NOT NULL,
    [Gid]              INT        NOT NULL,
    CONSTRAINT [PK__InTraini__3214EC073DC4B968] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InTrainings_Participants] FOREIGN KEY ([Pid]) REFERENCES [dbo].[Participants] ([Id]),
    CONSTRAINT [FK_InTrainings_Federations] FOREIGN KEY ([Fid]) REFERENCES [dbo].[Federations] ([Id])
);

CREATE TABLE [dbo].[ParticipantHistory] (
    [Id]               INT        IDENTITY (1, 1) NOT NULL,
    [Pid]              INT        NOT NULL,
    [Progression]      FLOAT (53) NOT NULL,
    [Turn]             FLOAT (53) NOT NULL,
    [Asset]            FLOAT (53) NOT NULL,
    [DataQuantity]     FLOAT (53) NOT NULL,
    [DataQuality]      FLOAT (53) NOT NULL,
    [ResourceQuantity] INT        NOT NULL,
    [Gid]              INT        NOT NULL,
    CONSTRAINT [PK__Particip__3214EC071399F095] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParticipantHistory_Participants] FOREIGN KEY ([Pid]) REFERENCES [dbo].[Participants] ([Id])
);

CREATE TABLE [dbo].[FederationHistory] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [Fid]             INT          NOT NULL,
    [Progression]     FLOAT (53)   NOT NULL,
    [Turn]            FLOAT (53)   NOT NULL,
    [Asset]           FLOAT (53)   NOT NULL,
    [TimeLeftInState] FLOAT (53)   NOT NULL,
    [State]           VARCHAR (50) NOT NULL,
    [MarketShare]     FLOAT (53)   NOT NULL,
    [ModelQuality]    FLOAT (53)   NOT NULL,
    [Gid]             INT          NOT NULL,
    CONSTRAINT [PK_FederationHistory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FederationHistory_Federations] FOREIGN KEY ([Fid]) REFERENCES [dbo].[Federations] ([Id])
);

CREATE TABLE [dbo].[FederationParticipantsHistory] (
    [Id]                    INT        IDENTITY (1, 1) NOT NULL,
    [Progression]           FLOAT (53) NOT NULL,
    [Turn]                  FLOAT (53) NOT NULL,
    [Pid]                   INT        NOT NULL,
    [ResourceCommitted]     INT        NOT NULL,
    [DataQuantityCommitted] FLOAT (53) NOT NULL,
    [DataQualityCommitted]  FLOAT (53) NOT NULL,
    [BidAmount]             FLOAT (53) NOT NULL,
    [Fid]                   INT        NOT NULL,
    [Gid]                   INT        NOT NULL,
    CONSTRAINT [PK_FederationParticipantsHistory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FederationParticipantsHistory_Federations] FOREIGN KEY ([Fid]) REFERENCES [dbo].[Federations] ([Id]),
    CONSTRAINT [FK_FederationParticipantsHistory_Participants] FOREIGN KEY ([Pid]) REFERENCES [dbo].[Participants] ([Id])
);

