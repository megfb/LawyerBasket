export interface PostDto {
  id: string;
  userId: string;
  content: string;
  likes?: LikesDto[];
  comments?: CommentDto[];
  createdAt: string;
  updatedAt?: string;
}

export interface CommentDto {
  id: string;
  userId: string;
  postId: string;
  text: string;
  createdAt: string;
  updatedAt?: string;
}

export interface LikesDto {
  id: string;
  userId: string;
  postId: string;
  createdAt: string;
  updatedAt?: string;
}

export interface PostLikeUserDto {
  likeId: string;
  userId: string;
  postId: string;
  firstName: string;
  lastName: string;
  profileImage?: string;
  createdAt: string;
}

