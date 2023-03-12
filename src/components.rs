use bevy::prelude::*;

#[derive(Component)]
pub struct Player {
    pub id: i8,
    pub position: [f32;3]
}

#[derive(Component)]
pub struct Sound {
    pub id: i8,
    pub min_distance: f32,
    pub max_distance: f32
}

