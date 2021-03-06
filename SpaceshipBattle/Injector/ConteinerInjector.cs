﻿using Autofac;
using SpaceshipBattle.Contracts;
using SpaceshipBattle.Contracts.Factories;
using SpaceshipBattle.Contracts.Providers;
using SpaceshipBattle.Core;
using SpaceshipBattle.Core.Factories;
using SpaceshipBattle.Core.Providers;
using SpaceshipBattle.DataBase;
using SpaceshipBattle.Core.RegistrationEntities;
using SpaceshipBattle.Core.Registration;
using SpaceshipBattle.Core.Services;
using SpaceshipBattle.Core.Services.ArmorServices;
using SpaceshipBattle.Core.Services.EngineServices;
using SpaceshipBattle.Core.Services.SpaceShipService;
using SpaceshipBattle.Entities.Spaceships;

namespace SpaceshipBattle.Injector
{
    class ContainerInjector : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            this.RegisterFactoryComponents(builder);
            this.RegisterCoreComponents(builder);
            this.RegisterProvidersComponents(builder);

            base.Load(builder);
        }

        //Register Factory Components
        private void RegisterFactoryComponents(ContainerBuilder builder)
        {
            builder.RegisterType<EngineFactory>().As<IEngineFactory>();
            builder.RegisterType<CreateBugattiW16Service>().Named<IEngineService>("bugatti w16");
            builder.RegisterType<CreateFerrariV12GTService>().Named<IEngineService>("ferrari v12 gt");
            builder.RegisterType<CreateH2OMotorService>().Named<IEngineService>("h2o motor");
            builder.RegisterType<CreateIonX3Service>().Named<IEngineService>("ion x3");
            builder.RegisterType<CreateTrabantMotorService>().Named<IEngineService>("trabant motor");
            builder.RegisterType<CreateVasimirPlasmaService>().Named<IEngineService>("vasimir plasma engine");
            builder.RegisterType<CreateVW19TDIService>().Named<IEngineService>("vw 1.9 tdi");
            builder.RegisterType<ArmourFactory>().As<IArmourFactory>();
            builder.RegisterType<CreateAerogelCoverService>().Named<IArmorService>("aerogel cover");
            builder.RegisterType<CreateAntiMatterFieldService>().Named<IArmorService>("anti matter field");
            builder.RegisterType<CreateBrickCageService>().Named<IArmorService>("brick cage");
            builder.RegisterType<CreateBubbleFieldService>().Named<IArmorService>("bubble field");
            builder.RegisterType<CreateFullerenesArmourService>().Named<IArmorService>("fullerenes armour");
            builder.RegisterType<CreatePlasmaFieldService>().Named<IArmorService>("plasma field");
            builder.RegisterType<CreateAntiMatterFieldService>().Named<IArmorService>("recycled paper");
            builder.RegisterType<CreateSwitzArmourService>().Named<IArmorService>("switz armour");
            builder.RegisterType<WeaponFactory>().As<IWeaponFactory>();
            builder.RegisterType<CreateAK47Service>().Named<IWeaponService>("ak47");
            builder.RegisterType<CreateCannonService>().Named<IWeaponService>("cannon");
            builder.RegisterType<CreateLaserService>().Named<IWeaponService>("laser");
            builder.RegisterType<CreatePlasmaWeaponService>().Named<IWeaponService>("plasma weapon");
            builder.RegisterType<SpaceshipFactory>().As<ISpaceshipFactory>();
            builder.RegisterType<CreateDrossMashupService>().Named<ISpaceShipService>("dross-mashup spaceship");
            builder.RegisterType<CreateFuturisticSpaceshipService>().Named<ISpaceShipService>("futuristic spaceship");
            builder.RegisterType<BulletFactory>().As<IBulletFactory>();
            builder.RegisterType<PlayerFactory>().As<IPlayerFactory>();
        }

        // Register Core Components
        private void RegisterCoreComponents(ContainerBuilder builder)
        {
            builder.RegisterType<Engine>().As<IEngine>();
            builder.RegisterType<Registration>().As<IRegistration>();
            builder.RegisterType<PlayerCreator>().As<IPlayerCreator>();
            builder.RegisterType<GameController>().As<IGameController>();
            builder.RegisterType<SelectingSpaceship>().As<ISelectingSpaceship>();
            builder.RegisterType<FilterComponents>().As<IFilterComponents>();
            builder.RegisterType<DrawShip>().As<IDrawShip>().SingleInstance();
            builder.RegisterType<SpaceShipDesign>().As<ISpaceShipDesign>();

        }

        // Register Providers Components
        private void RegisterProvidersComponents(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleWriter>().As<IWriter>();
            builder.RegisterType<ConsoleReader>().As<IReader>();
            builder.RegisterType<LocalDataBase>().As<IDataBase>();
            builder.RegisterType<ConsoleApplicationInterface>().As<IApplicationInterface>();
            builder.RegisterType<Menu>().As<IMenu>();
        }
    }
}
